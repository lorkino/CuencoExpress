import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class GeneralService {

  constructor() { }

  getUser(base64: string): File {
    // Base64 url of image
    // Naming the image
    const date = new Date().valueOf();
    let text = '';
    const possibleText = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (let i = 0; i < 5; i++) {
      text += possibleText.charAt(Math.floor(Math.random() * possibleText.length));
    }
    // Replace extension according to your media type
    const imageName = date + '.' + text + '.jpeg';
    // call method that creates a blob from dataUri
    const imageBlob = this.dataURItoBlob(base64);
    const imageFile = new File([imageBlob], imageName, { type: 'image/jpeg' });
    return imageFile;
    

  }
  dataURItoBlob(dataURI) {
      const byteString = atob(dataURI);
      const arrayBuffer = new ArrayBuffer(byteString.length);
      const int8Array = new Uint8Array(arrayBuffer);
      for (let i = 0; i < byteString.length; i++) {
        int8Array[i] = byteString.charCodeAt(i);
      }
      const blob = new Blob([arrayBuffer], { type: 'image/jpeg' });
      return blob;
    }


}
