import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/product';
import { type } from '../shared/models/type';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/ShopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent  implements OnInit{
@ViewChild('search') searchTerm?: ElementRef;
products : Product [] = [];
brand  : Brand [] =  [];
type : type [] = [];
shopParams = new ShopParams();
sortOptions = [
  {name : 'Alphabetical',value: 'name'},
  {name : 'Price Low to high',value : 'PriceAsc'},
  {name : 'Price high to Low ',value : 'PriceDesc'},

];

totalCount = 0;

constructor (private shopService: ShopService) {}


  ngOnInit(): void {
  this.getproducts ();
  this.getBrands();
  this.getTypes();
    
    
  }

  getproducts(){
      this.shopService.getProducts(this.shopParams).subscribe({
      next: response  => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize =response.pageSize;
        this.totalCount = response.count;
      },
      error: error => console.log(error)
    })

  }

  getBrands(){
    this.shopService.getBrands().subscribe({
     next : response => this.brand = [{id: 0, name: 'All'},...response], 
     error : error => console.log(error)
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
     next : response => this.type = [{id: 0, name: 'All'},...response], 
     error : error => console.log(error)
    })
  }

OnBrandSelected (brandId : number) {
  this.shopParams.brandId = brandId;
  this.shopParams.pageNumber = 1;
  this.getproducts();
}

OnTypeSelected (typeId : number) {
  this.shopParams.typeId = typeId;
  this.shopParams.pageNumber = 1;
  this.getproducts();

}


onSortSelected(event : any) {
  this.shopParams.sort = event.target.value;
  this.getproducts();
}

onpageChanged (event: any){
  if (this.shopParams.pageNumber !== event.page){
    this.shopParams.pageNumber = event;
    this.getproducts();
  }
}

onSearch() {

  this.shopParams.search = this.searchTerm?.nativeElement.value;
  this.shopParams.pageNumber = 1;
  this.getproducts();



  }

  onReset (){
    if (this.searchTerm) this.searchTerm.nativeElement.value= '';
    this.shopParams = new ShopParams();
    this.getproducts();
  }

 
  
}




