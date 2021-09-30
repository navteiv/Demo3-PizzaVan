import { Component, OnInit, ViewChild } from '@angular/core';
import { SystemConstants } from 'src/app/core/common/constants';
import { DataService } from 'src/app/core/services/data.service';
import { Cart, Item } from '../viewCart';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css', '../../../assets/css/templatemo-sixteen.css', '../../../assets/css/owl.css','../../../assets/css/cart.css']
})
export class IndexComponent implements OnInit {
  @ViewChild('addModal') public addModal!: ModalDirective

  public Cart!: Cart;
  public Item!: Item;
  public imagePath = SystemConstants.LOCAL_API + "Image/Dish/"
  public p: number = 1;
  public sum: number = 0;
  public products!: any[];

  constructor(private dataService: DataService) {
    localStorage.setItem("localCart", JSON.stringify(this.cartItems));
  }
  ngOnInit(): void {
    this.loadData();
    this.cartItems = JSON.parse(localStorage.getItem("localCart")!);
  }
  ngDoCheck(): void {
    if(this.cartItems !== null){
      this.cartItems = JSON.parse(localStorage.getItem("localCart")!);
      let temp=0;
      for(let i = 0; i<this.cartItems.length; i++){
        temp += this.cartItems[i].Total;
      }
      this.sum = temp;
    }
  }
  loadData(): void{
    this.dataService.get('api/Dish').subscribe(
      (response: any) => {
        this.products = response;
        console.log(this.products);
      }
    )
  }
  public cartItems?: any = [];
  checkOut(): void{
    this.Cart = {
      CusId: parseInt(localStorage.getItem("cusId")!),
      ListViewCart: this.cartItems,
      TotalPrice: this.sum
    }
    if(localStorage.getItem("jwt")){
      this.dataService.post('api/Cart', JSON.stringify(this.Cart))
      .subscribe((response: any) => {
        console.log(response);
        localStorage.removeItem("localCart");
        localStorage.removeItem("countCartItem");
      })
    }
    else{ alert("Vui lòng đăng nhập")}
  }
  addToCart(prod: any): void{
    this.Item = {
      Dish: prod,
      Quantity: 1,
      Total: prod.price
    }
    console.log(this.Item);
    let cartData = localStorage.getItem("localCart");
    console.log(cartData);
    if(cartData == null){
      let storeDataCart: any = [];
      storeDataCart.push(this.Item);
      localStorage.setItem("localCart", JSON.stringify(storeDataCart));
    }
    else
    {
      this.cartItems.push(this.Item);
      localStorage.setItem("localCart", JSON.stringify(this.cartItems));
    }
    localStorage.setItem("countCartItem", JSON.stringify(this.cartItems.length));
  }
  removeCart(item: any){
    this.cartItems = this.cartItems.filter((prod: any) => prod !== item);
    localStorage.setItem("localCart", JSON.stringify(this.cartItems));
    localStorage.setItem("countCartItem", JSON.stringify(this.cartItems.length));
  }
  showModal(): void{
    this.addModal.show();

  }
}
