import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Product } from 'src/app/model/product';
import { ProductService } from 'src/app/services/product.service';
import { ProductDialogComponent } from './product-dialog/product-dialog.component';

@Component({
  selector: 'app-productAdmin',
  templateUrl: './productAdmin.component.html',
  styleUrls: ['./productAdmin.component.css']
})
export class ProductAdminComponent implements OnInit {

  dataSource = new MatTableDataSource<Product>();
  displayedColumns = [ 'id', 'productName', 'barcodeNumber','price','description','quantity', "actions"];
  selection = new SelectionModel<Product>(true, []);

  @ViewChild(MatPaginator)paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor( private productService: ProductService,public dialog: MatDialog, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.LoadData();
  }

  LoadData() {
    this.productService.getProducts().subscribe(data => {
      this.dataSource = new MatTableDataSource<Product>(data);
      console.log(data);
      this.dataSource.paginator = this.paginator;
      setTimeout(() => this.dataSource.sort = this.sort);
      this.dataSource.filterPredicate = (data: Product, filterValue: string) =>
        data.productName.toLocaleLowerCase().indexOf(filterValue.toLocaleLowerCase()) !== -1
        || data.id.toString().toLocaleLowerCase().indexOf(filterValue.toLocaleLowerCase()) !== -1;
      this.selection.clear();
    });
  }

  
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */

  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  actions(action:any, obj:any) {
    if (action === "Güncelle" || action === "Resimleri") {
      this.productService.getProductById(obj.id).subscribe(data => {
        this.openDialog(action, data);
      });
    } else {
      this.openDialog(action, obj);
    }
  }


  openDialog(action:any, obj:any) {
    obj.action = action;
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Ekle') {
        console.log(result.data);
        this.productService.add(result.data).subscribe(data => {
          console.log(data);
          this._snackBar.open(data.body.message, "Tamam", { duration: 5000, });
          this.LoadData();
        }, error => {
          console.log(error);
          this._snackBar.open("Hata : " + error.error.message, "Tamam", { duration: 8000, });
        });


      } else if (result.event == 'Güncelle') {
        console.log(result.data);
        this.productService.update(result.data).subscribe(data => {
          console.log(data);
          this._snackBar.open(data.body.message, "Tamam", { duration: 5000, });
          this.LoadData();
        }, error => {
          console.log(error);
          this._snackBar.open("Hata : " + error.error.message, "Tamam", { duration: 8000, });
        });

      } else if (result.event == 'Sil') {
        console.log(result.data);
        this.productService.delete(result.data).subscribe(data => {
          console.log(data);
          this._snackBar.open(data.body.message, "Tamam", { duration: 5000, });
          this.LoadData();
        }, error => {
          console.log(error);
          this._snackBar.open("Hata : " + error.error.message, "Tamam", { duration: 8000, });
        });
      } else if (result.event == 'Vazgeç') {
        this._snackBar.open("Vazgeçildi", "Tamam", { duration: 2000, });
      }
    });
  }


}
