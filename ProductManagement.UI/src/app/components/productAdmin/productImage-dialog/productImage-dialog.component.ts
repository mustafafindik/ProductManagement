import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Inject, OnInit, Optional, Output } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Product } from 'src/app/model/product';
import { UploadImagesService } from 'src/app/services/uploadImages.service';

@Component({
  selector: 'app-productImage-dialog',
  templateUrl: './productImage-dialog.component.html',
  styleUrls: ['./productImage-dialog.component.css']
})
export class ProductImageDialogComponent implements OnInit {
  selectedFiles!: FileList;
  progressInfos:any = [];
  message = '';
  fileInfos!: Observable<any>;
  id:number;
  local_data:any;

  public progress!: number;
  @Output() public onUploadFinished = new EventEmitter();
  
  constructor(@Optional() @Inject(MAT_DIALOG_DATA) public data: Product,private uploadService: UploadImagesService) { 
    this.local_data = {...data};
    this.id = this.local_data.id;
  }

  ngOnInit() {
    this.fileInfos = this.uploadService.getFiles(this.id);
  }

  selectFiles(event:any) {
    this.progressInfos = [];
  
    const files = event.target.files;
    let isImage = true;
  
    for (let i = 0; i < files.length; i++) {
      if (files.item(i).type.match('image.*')) {
        continue;
      } else {
        isImage = false;
        alert('invalid format!');
        break;
      }
    }
  
    if (isImage) {
      this.selectedFiles = event.target.files;
    } else {
      this.selectedFiles;
      event.srcElement.percentage = null;
    }
  }

  uploadFiles() {
    this.message = '';
  
    for (let i = 0; i < this.selectedFiles.length; i++) {
      this.upload(this.selectedFiles[i]);
    }
  }


 
  upload(file:any) {
    if (file.length === 0) {
      return;
  }

  const fileToUpload = file[0] as File;
  const formData = new FormData();
  formData.append('file', file);

  this.uploadService.uploadFile(formData)
      .subscribe(event => {
          if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round(100 * event.loaded / event.total);
          } else if (event.type === HttpEventType.Response) {
              this.message = 'Upload success.';
              this.onUploadFinished.emit({ Message: event.body.dbPath});
          }
      });
  }

}
