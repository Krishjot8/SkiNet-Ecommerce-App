import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, BasketItem, BasketTotals } from '../shared/models/basket';
import { Product } from '../shared/models/product';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/DeliveryMethod';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environment.apiUrl; //using environment api url of development
  private basketSource = new BehaviorSubject<Basket | null>(null); //Basket service will initialize array of items in basket or null if no items in basket
  basketSource$ = this.basketSource.asObservable(); //the component file will subscribe to basketSource$
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {}

  createPaymentIntent() {
    return this.http
      .post<Basket>(
        this.baseUrl + 'payment/' + this.getCurrentBasketValue()?.id,
        {}
      )
      .pipe(
        map((basket) => {
          this.basketSource.next(basket);
        })
      );
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    const basket = this.getCurrentBasketValue();

    if (basket) {
      basket.shippingPrice = deliveryMethod.price;
      basket.deliveryMethodId = deliveryMethod.id;
      this.setBasket(basket);
    }
    this.calculateTotals();
  }
  getBasket(id: string) {
    return this.http.get<Basket>(this.baseUrl + 'basket?id=' + id).subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      },
    });
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next: (basket) => {
        this.basketSource.next(basket);
        this.calculateTotals();
      },
    });
  }

  getCurrentBasketValue() {
    //It will get value of the basket
    return this.basketSource.value;
  }

  addItemToBasket(item: Product | BasketItem, quantity = 1) {
    //adding item to basketArray
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item); //initialized all the mapping items to the itemToAdd property
    const basket = this.getCurrentBasketValue() ?? this.createBasket(); //initialize the getCurrentBasketValue to basket property if basket doen't exist then create a basket
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  removeItemFromBasket(id: number, quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const item = basket.items.find((x) => x.id === id);
    if (item) {
      item.quantity -= quantity;

      if (item.quantity === 0) {
        basket.items = basket.items.filter((x) => x.id !== id);
      }

      if (basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }
  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe({
      next: () => {
        this.deleteLocalBasket();
      },
    });
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

  private addOrUpdateItem(
    items: BasketItem[],
    itemToAdd: BasketItem,
    quantity: number
  ): BasketItem[] {
    const item = items.find((x) => x.id === itemToAdd.id);
    if (item) item.quantity += quantity; //increase the existing item quantity
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }
  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product): BasketItem {
    //we are mapping the product model to basketitem model

    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType,
    };
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;

    const subtotal = basket.items.reduce((a, b) => b.price * b.quantity + a, 0); //we will take our array of items multiply the price by the quantity and get the sum total of all the items.
    const total = subtotal + basket.shippingPrice;
    this.basketTotalSource.next({shipping: basket.shippingPrice,  total,  subtotal});
  }

  private isProduct(item: Product | BasketItem): item is Product {
    return (item as Product).productBrand !== undefined;
  }
}
