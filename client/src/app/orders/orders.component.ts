import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { OrdersListParams } from '../shared/models/ordersListParams';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  orders: IOrder[];
  ordersListParams = new OrdersListParams();
  totalCount: number;

  constructor(private ordersService: OrdersService) { }

  ngOnInit(): void {
    this.getOrdersForUser();
  }

  getOrdersForUser() {
    this.ordersService.getOrders(this.ordersListParams).subscribe({
      next: (response) => {
        this.orders = response.data;
        this.ordersListParams.pageNumber = response.pageIndex;
        this.ordersListParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  onPageChanged(event: any) {
    if (this.ordersListParams.pageNumber !== event) {
      this.ordersListParams.pageNumber = event;
      this.getOrdersForUser();
    }
  }

  sortSelected(sort: string) {
    this.ordersListParams.orderReversed = !this.ordersListParams.orderReversed;
    this.ordersListParams.sort = sort;
    console.log(sort);
    this.getOrdersForUser();
  }

  onSearch() {
    this.ordersListParams.search = this.searchTerm.nativeElement.value;
    this.ordersListParams.pageNumber = 1;
    this.getOrdersForUser();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.ordersListParams = new OrdersListParams();
    this.getOrdersForUser();
  }

}
