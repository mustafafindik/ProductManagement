import { HttpClient, HttpEvent, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Settings } from '../helper/Settings';

@Injectable({
  providedIn: 'root'
})
export class UploadImagesService {

  path = Settings.ApiBaseUrl;

  constructor(private http: HttpClient) { }

  upload(file: File): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    const req = new HttpRequest('POST', `${this.path}products/UploadImages`, formData, {
      reportProgress: true,
     
    });

    

    return this.http.request(req);
  }

  getFiles(id:number): Observable<any> {
    return this.http.get(`${this.path}products/images/`+id);
  }

  uploadFile(formData: FormData): Observable<any> {
    return this.http.post(`${this.path}products/UploadImages`, 
    formData, {reportProgress: true, observe: 'events'})
        ;
}
}
