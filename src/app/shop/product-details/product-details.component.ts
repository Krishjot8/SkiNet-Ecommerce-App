import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;
  quantity = 1;
  quantityInBasket = 0;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  //subscribe is to use it many times like subscribing to a channel and we can do it for the add, update and delete methods

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: (product) => {
          //next means do what will come next after loading product

          this.product = product;
          this.bcService.set('@productDetails', product.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            //we will take one value from the basketsource then unsubscribe it.
            next: (basket) => {
              const item = basket?.items.find((x) => x.id === +id);
              if (item) {
                //if we have the item

                this.quantity = item.quantity; //update the prodDetails page with increment and decrement buttons.
                this.quantityInBasket = item.quantity;
              }
            },
          });
        },

        error: (error) => console.log(error),
      });
  }


incrementQuantity(){

this.quantity++

}

decrementQuantity(){

  this.quantity--

  }

  updateBasket(){

if(this.product){

if(this.quantity > this.quantityInBasket) {

  const itemsToAdd = this.quantity - this.quantityInBasket;
this.quantityInBasket += itemsToAdd; //we will update the quantity in basket before we call the basketService
this.basketService.addItemToBasket(this.product, itemsToAdd);//calling the basketService to add item to basket
}  else{
    const itemsToRemove = this.quantityInBasket - this.quantity;                //we are removing from the basket
  this.quantityInBasket -= itemsToRemove;
  this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
    }
  }
}

get buttonText(){

  return this.quantityInBasket === 0 ? 'Add to basket' : 'Update Basket'


}


}
