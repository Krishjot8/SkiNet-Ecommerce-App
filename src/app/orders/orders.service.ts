import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Order } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.apiUrl;

  GetOrdersForUser() {
    //get all orders

    return this.http.get<Order[]>(this.baseUrl + 'orders');
  }

  GetOrderDetailed(id: number) {
    //get single order details

    return this.http.get<Order>(this.baseUrl + 'orders/' + id);
  }
}
