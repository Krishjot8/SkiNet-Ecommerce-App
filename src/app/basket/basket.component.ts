import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { BasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketService: BasketService){}


incrementQuantity(item: BasketItem){

this.basketService.addItemToBasket(item)

}

removeItem(id : number, quantity: number){

this.basketService.removeItemFromBasket(id , quantity); //we are calling the service method here to remove the item from the basket as well as the quantity

}


}
