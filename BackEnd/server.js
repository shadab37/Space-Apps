var app 			= require('express')();
var http 			= require('http').Server(app);
var io 				= require('socket.io')(http);
var base64 		= require('node-base64-image');
var Promise 	= require('promise');
var waterfall = require('async-waterfall');


var Clarifai 	= require('./clarifai_node.js');

var clientID 	= "yNGG_Nxg9HrT5-9v1aCT99IF2mH_NHSzw6TNqZva";
var clientSec = "Ccw5CtVqNXsxa3jzgzFCzM4J4GWdrJoCwIabCNtb";

Clarifai.initAPI(clientID, clientSec);

var randomWords = ['cat', 'dog', 'basketball', 'car', 'airplane', 'park', 'house', 'computer', 'man', 'woman', 'balloon', 'carrot', 'banana', 'table', 'water', 'sun', 'flower', 'chair', 'tree', 'city'];

app.get('/', function(req, res) {
	res.sendFile(__dirname + '/index.html');
});

function getProbs(data, class_name, ourId) {
	return new Promise(function (resolve, reject) {
		Clarifai.getProbsEncodedData(data , class_name, ourId, function(err, res) {
			if(err) {
				reject(err);
			} else {
				resolve(res);
			}
		});
	});
}

// socketId -> socketId
var clients = {};
// Array
var socketIds = [];
// Queue
var pool = [];
// socketId -> socketId
var pairs = {};
// socketId -> prob
var probMap = {};

// Print data stores
function printStuff() {
	console.log('all clients');
	for(var propertyName in clients) {
 		console.log(propertyName);
	}
	console.log('all socketIds');
	console.log(socketIds);
	console.log('-------------');
	console.log('pool');
	console.log(pool);
	console.log('-------------');
	console.log('pairs');
	console.log(pairs);
	console.log('-------------');
	console.log('\n\n\n\n\n');
}

// Connection started
io.on('connection', function(socket) {
	console.log('a user connected');
	console.log(socket.id);

	// Add client to clients object
	clients[socket.id] = socket;
	// Add id to array
  socketIds.push(socket.id);

  socket.on('findMatch', function() {
  	console.log(socket.id + " wants to find a match.");
  	pool.push(socket.id);

  	if (pool.length >= 2) {
  		console.log("Match found!");
  		// Get from pool
  		var one = pool.shift();
  		var two = pool.shift();
  		// Add to map
  		pairs[one] = two;
  		pairs[two] = one;

  		var randomWord = randomWords[Math.floor(Math.random() * randomWords.length)];
  		// Emit messages
  		clients[one].emit('findMatch', {"word": randomWord});
  		clients[two].emit('findMatch', {"word": randomWord});
  	}
    printStuff();
  });

  socket.on('soloMatch', function() {
  	console.log(socket.id + " wants to play single player.");
  	var randomWord = randomWords[Math.floor(Math.random() * randomWords.length)];
  	socket.emit('soloMatch', {"word": randomWord});
  });

  // Remove socket from array when user disconencts
  socket.on('disconnect', function () {
  	console.log('a user disconnected');
		waterfall([
			function(callback) {
				socketIds.splice(socketIds.indexOf(socket.id), 1);		
				callback();
			}, 
			function(callback) {
				pool.splice(socketIds.indexOf(socket.id), 1);
				callback();
			}, 
			function(callback) {
				delete clients[socket.id];
				callback();
			}, 
			function(callback) {
				// If is in the pairs map
				if (pairs[socket.id]) {
					var other = pairs[socket.id];
					delete pairs[socket.id];
					delete pairs[other];
				} 
				callback();
			},		
			function(callback) {
				console.log('STUFF');
				printStuff();
			}	
		]); 
  });

  socket.on('imageProb', function(image, word) {
    
  	getProbs(image, word, socket.id).then(function(res) {
  		socket.emit('imageProb', {
  			"result": Math.max.apply(null, res.results[0].result.tag.probs),
        "word": word
  		});
  		console.log(Math.max.apply(null, res.results[0].result.tag.probs));
  	}).catch(function(err) {
  		console.log(err);
  	});
  });
	
  socket.on('image1v1', function(image, word) {
  	getProbs(image, word, socket.id).then(function(res) {
  		probMap[socket.id] = Math.max.apply(null, res.results[0].result.tag.probs);
  		// Both submitted
  		if (probMap[pairs[socket.id]]) {
  			var one = probMap[socket.id];
  			var two = probMap[pairs[socket.id]];
  			console.log("Player1: " + one);
  			console.log("Player2: " + two);
  			if (one > two) {
  				clients[socket.id].emit('image1v1', {
  					won: true,
  					yours: one,
  					theirs: two
  				});
  				clients[pairs[socket.id]].emit('image1v1', {
  					won: false,
  					yours: two,
  					theirs: one
  				});
  			} else {
  				clients[socket.id].emit('image1v1', {
  					won: false,
  					yours: one,
  					theirs: two
  				});
  				clients[pairs[socket.id]].emit('image1v1', {
  					won: true,
  					yours: two,
  					theirs: one
  				});
  			}
  			
  			// Clean-up
  			// Delete prob results
  			delete probMap[socket.id];
  			delete probMap[pairs[socket.id]];
  			
  			// Delete pairs
  			delete pairs[pairs[socket.id]];
  			delete pairs[socket.id];
  		}
  	}).catch(function(err) {
  		console.log(err);
  	});
  });

});

http.listen(3000, function(){
  console.log('listening on *:3000');
});
	
