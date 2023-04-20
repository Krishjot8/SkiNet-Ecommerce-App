import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../orders.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Order } from 'src/app/shared/models/order';

@Component({
  selector: 'app-orders-detailed',
  templateUrl: './orders-detailed.component.html',
  styleUrls: ['./orders-detailed.component.scss']
})
export class OrdersDetailedComponent implements OnInit {

  order?: Order;

  constructor(private ordersService: OrdersService ,private route: ActivatedRoute,
    private bcService: BreadcrumbService){
  }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    id && this.ordersService.GetOrderDetailed(+id).subscribe({
    next: order => {
    this.order = order;
    this.bcService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
  }


    })



  }



}
