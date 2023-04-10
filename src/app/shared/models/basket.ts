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
}

export class Basket implements Basket{ //When creating a new instance of a basket
id = cuid();

items: BasketItem[] = [];

}

export interface BasketTotals{

  shipping: number;
  subtotal: number;
  total: number;

}
