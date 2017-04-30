var express        =        require("express");
var multer  = require('multer')
var upload = multer({ dest: 'uploads/' })
var app            =        express();
var Clarifai = require('clarifai');
var fs = require('fs');
var _ = require('underscore-node');
app.set('view engine', 'ejs');
 
// Load method categories.
var knownAnimals = ["duck", "dog", "cat", "turtle", "pigeon", "deer", "coyote", "snake", "vulture"]

// function to encode file data to base64 encoded string
function base64_encode(file) {
    // read binary data
    var bitmap = fs.readFileSync(file);
    // convert binary data to base64 encoded string
    return new Buffer(bitmap).toString('base64');
}

app.post('/animal',upload.single('photo'), function(request,response){
  //var responseString = {"animal": "duck", "points": "5", "total": "23"};
  //console.log (responseString)
  //response.status(200).send(responseString)
  //return

  //var userid=request.body.userid;
  var photo=request.file;

  var clarifyApp = new Clarifai.App(
    '2IakXrvJ2SjaYIhDOtiEWGk6cMAt0wIsFkqRjfCQ',
    'yaIoSFqnZ5ZVrtQK52l6hAINcCEiAi7xYCno4iLg'
  );

  var base64strOfImageFile = base64_encode(photo.path);
  var responseString = ""

  clarifyApp.models.predict(Clarifai.GENERAL_MODEL, {base64: base64strOfImageFile}).then(
    function(clarifaiResponse) {
      var concepts = clarifaiResponse.outputs[0].data.concepts
      var animal = _.find(concepts, function(concept){
          return _.contains(knownAnimals, concept.name)
      })

      if (animal) {
        responseString = {"animal": animal.name, "points": "5", "total": 23}
      } else {
        responseString = {"animal": "", "points": "0", "total": 18}
      }

      console.log (responseString)
      //response.status(200).send(responseString)
      response.render('response', {
          animal: animal.name,
          points: 5
        })
    },
    function(err) {
      console.log ("error")
      responseString = {"animal": "", "points": 0, "total": 18}
    }
  );
});

app.listen(3000, function(){
  console.log('listening on *:3000');
});

