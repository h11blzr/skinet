import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
  order: IOrder;
  orderId: number;

  constructor(private ordersService: OrdersService,
              private activateRoute: ActivatedRoute,
              private bcService: BreadcrumbService) {
    this.bcService.set('@orderId', ' ');
  }

  ngOnInit(): void {
    this.getOrderForUser();
  }

  getOrderForUser() {
    this.ordersService.getOrder(+this.activateRoute.snapshot.paramMap.get('id')).subscribe({
      next: (response) => {
        this.order = response;
        this.bcService.set('@orderId', 'Order №' + this.order.id.toString());
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

}
