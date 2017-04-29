//
//  CameraViewController.swift
//  SnapchatSwipeView
//
//  Created by Zhongheng Li on 4/14/17.
//  Copyright © 2017 Brendan Lee. All rights reserved.
//

import Foundation
import UIKit
import AVFoundation


@available(iOS 10.0, *)
class CameraViewController: UIViewController {
    
    
    var session = AVCaptureSession()
    var stillImageOutput = AVCapturePhotoOutput()
    var videoPreviewLayer = AVCaptureVideoPreviewLayer()
    
    
    
    @IBOutlet weak var previewView: UIView!
    
    @IBOutlet weak var takePicture: UIButton!
  
    
    override func viewDidLoad() {
        super.viewDidLoad()
    }
    
    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    
 
    
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
     
        let backCamera = AVCaptureDevice.defaultDevice(withMediaType: AVMediaTypeVideo)
        
        session = AVCaptureSession()
        session.sessionPreset = AVCaptureSessionPresetHigh
        
        
        
        var error: NSError?
        var input: AVCaptureDeviceInput!
        do {
            input = try AVCaptureDeviceInput(device: backCamera)
        } catch let error1 as NSError {
            error = error1
            input = nil
            print(error!.localizedDescription)
        }
        
        if error == nil && session.canAddInput(input) {
            session.addInput(input)
            // ...
            // The remainder of the session setup will go here...
            
           // stillImageOutput = AVCaptureStillImageOutput()
           // stillImageOutput?.outputSettings = [AVVideoCodecKey: AVVideoCodecJPEG]
            
            if session.canAddOutput(stillImageOutput) {
                session.addOutput(stillImageOutput)
                // ...
                // Configure the Live Preview here...
                
                videoPreviewLayer = AVCaptureVideoPreviewLayer(session: session)
                videoPreviewLayer.videoGravity = AVLayerVideoGravityResizeAspect
                videoPreviewLayer.connection.videoOrientation = AVCaptureVideoOrientation.portrait
                previewView.layer.addSublayer(videoPreviewLayer)
                previewView.addSubview(takePicture)
                
                videoPreviewLayer.position = CGPoint (x: self.previewView.frame.width/2, y: self.previewView.frame.height/2)
                videoPreviewLayer.bounds = previewView.frame
                
                session.startRunning()
            }
            
            
            
            
        }
        
        
    }
    
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        videoPreviewLayer.frame = previewView.bounds
    }
  
}
