import * as cuid from "cuid";


export interface BasketItem { // the item and its properties
  id:          number;
  productName: string;
  price:       number;
  quantity:    number;
  pictureUrl:  string;
  brand:       string;
  type:        string;
}

export interface Basket { //the basket with list of items
  id:    string;
  items: BasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice: number;
}

export class Basket implements Basket{ //When creating a new instance of a basket
id = cuid();
items: BasketItem[] = [];
shippingPrice = 0

}

export interface BasketTotals{

  shipping: number;
  subtotal: number;
  total: number;

}
