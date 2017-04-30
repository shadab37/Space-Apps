//
//  ViewController.swift
//  Animx
//
//  Created by hackathon on 4/29/17.
//  Copyright © 2017 Hackathon. All rights reserved.
//

/*
 let alertController = UIAlertController(title: "Animx", message:
 "Hello, SpaceApps!", preferredStyle: UIAlertControllerStyle.alert)
 alertController.addAction(UIAlertAction(title: "Dismiss", style: UIAlertActionStyle.default,handler: nil))
 
 self.present(alertController, animated: true, completion: nil)
 */




/*
 let urlString = URL(string: "http://jsonplaceholder.typicode.com/users/1")
 if let url = urlString {
 let task = URLSession.shared.dataTask(with: url) { (data, response, error) in
 if error != nil {
 print(error)
 } else {
 if let usableData = data {
 print(usableData) //JSONSerialization
 }
 }
 }
 task.resume()
 }
 */

/*
 var request = URLRequest(url: URL(string: "http://127.0.0.1/~hackathon/animal5")!)
 request.httpMethod = "POST"
 let postString = "id=13&name=Jack"
 request.httpBody = postString.data(using: .utf8)
 let task = URLSession.shared.dataTask(with: request) { data, response, error in
 guard let data = data, error == nil else {                                                 // check for fundamental networking error
 print("error=\(error)")
 return
 }
 
 if let httpStatus = response as? HTTPURLResponse, httpStatus.statusCode != 200 {           // check for http errors
 print("statusCode should be 200, but is \(httpStatus.statusCode)")
 print("response = \(response)")
 }
 
 do {
 if let data = String(data: response, enconding: NSUTF8StringEncoding),
 let json = try JSONSerialization.jsonObject(with: data) as? [String: Any],
 let blogs = json["blogs"] as? [[String: Any]] {
 for blog in blogs {
 if let name = blog["name"] as? String {
 names.append(name)
 }
 }
 }
 } catch {
 print("Error deserializing JSON: \(error)")
 }
 
 let responseString = String(data: data, encoding: .utf8)
 print("responseString = \(responseString)")
 }
 task.resume()
 
 print ("hoho")
 */




import UIKit

class ViewController: UIViewController, UIImagePickerControllerDelegate,
UINavigationControllerDelegate {
    //MARK: Properties
    @IBOutlet weak var myImageView: UIImageView!
    @IBOutlet weak var uploadButton: UIButton!
    //@IBOutlet weak var imagePicked: UIImageView!
    let picker = UIImagePickerController()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        picker.delegate = self
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    //MARK: Actions
    @IBAction func openPhotoLibrary(_ sender: Any) {

        /*
        if UIImagePickerController.isSourceTypeAvailable(UIImagePickerControllerSourceType.photoLibrary) {
            let imagePicker = UIImagePickerController()
            imagePicker.delegate = self
            imagePicker.sourceType = UIImagePickerControllerSourceType.photoLibrary;
            imagePicker.allowsEditing = false
            self.present(imagePicker, animated: true, completion: nil)
        }
        */
        
        picker.allowsEditing = false
        picker.sourceType = .photoLibrary
        picker.mediaTypes = UIImagePickerController.availableMediaTypes(for: .photoLibrary)!
        present(picker, animated: true, completion: nil)
        
        
        // This shows how you can specify the settings/parameters instead of using the default/shared parameters
        let urlToRequest = "http://127.0.0.1/~hackathon/animal"
        func dataRequest() {
            let url4 = URL(string: urlToRequest)!
            let session4 = URLSession.shared
            let request = NSMutableURLRequest(url: url4)
            request.httpMethod = "POST"
            request.cachePolicy = NSURLRequest.CachePolicy.reloadIgnoringCacheData
            let paramString = "data=Hello"
            request.httpBody = paramString.data(using: String.Encoding.utf8)
            let task = session4.dataTask(with: request as URLRequest) { (data, response, error) in
                guard let _: Data = data, let _: URLResponse = response, error == nil else {
                    print("*****error")
                    return
                }
                let dataString = NSString(data: data!, encoding: String.Encoding.utf8.rawValue)
                //print("*****This is the data 4: \(dataString)") //JSONSerialization
                //print(data)
                
                if error != nil {
                    print(error)
                } else {
                    do {
                        
                        let parsedData = try JSONSerialization.jsonObject(with: data!, options: []) as! [String:Any]
                        //print(parsedData)
                        //print(parsedData["points"])
                        //let currentConditions = parsedData["animal"] as! [String:Any]
                        let animal = parsedData["animal"] as! NSString
                        let currentConditions = (parsedData["points"] as! NSString).integerValue
                        
                        print(currentConditions)
                        print(animal)
                        
                        //let currentTemperatureF = currentConditions["points"] as! Double
                        //print(currentTemperatureF)
                    } catch let error as NSError {
                        print(error)
                    }
                }
                
            }
            task.resume()
        }
        dataRequest()
        
    }
    
   /*
   func imagePickerController(_ picker: UIImagePickerController, didFinishPickingMediaWithInfo info: [String : Any]) {
        print("\(#function)")
    }
   */

    func imagePickerController(_ picker: UIImagePickerController,
                               didFinishPickingMediaWithInfo info: [String : AnyObject]) {
        let chosenImage = info[UIImagePickerControllerOriginalImage] as! UIImage //2
        myImageView.contentMode = .scaleAspectFit //3
        myImageView.image = chosenImage //4
        dismiss(animated:true, completion: nil) //5
    }
    
    func imagePickerControllerDidCancel(_ picker: UIImagePickerController) {
        dismiss(animated: true, completion: nil)
    }
    
    
}

