import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Product } from 'src/app/model/product';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.css']
})
export class ProductDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ProductDialogComponent>,
    //@Optional() is used to prevent error if no data is passed
    @Optional() @Inject(MAT_DIALOG_DATA) public data: Product, private formBuilder: FormBuilder,) {
  
    this.local_data = {...data};
    this.action = this.local_data.action;
    this.createForm();
    this.dialogRef.disableClose = true;
  
    
  }

  action:string;
  local_data:any;
  productAddForm!: FormGroup;


  createForm() {
    this.productAddForm = new FormGroup({
      productName: new FormControl(this.local_data.productName, [
        Validators.required,      
      ]),
      barcodeNumber: new FormControl(this.local_data.barbodeNumber, [
        Validators.required,      
      ]),
      price : new FormControl(this.local_data.price),
      description : new FormControl(this.local_data.description),
      quantity : new FormControl(this.local_data.quantity),
    });

  }

  doAction(){
    if(this.action !="Sil"){
      if(this.productAddForm.valid){
        this.dialogRef.close({event:this.action,data:this.local_data});
        }else{
          console.log("Not Valid")
        }
    }else{
      this.dialogRef.close({event:this.action,data:this.local_data});

    }
   
  }

  closeDialog(){
    this.dialogRef.close({event:'Vazgeç'});
  }

}
