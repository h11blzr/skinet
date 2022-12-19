import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IOrder } from '../shared/models/order';
import { OrdersListParams } from '../shared/models/ordersListParams';
import { IOrdersListPagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getOrders(ordersListParams: OrdersListParams) {
    let params = new HttpParams();

    if (ordersListParams.search) {
      params = params.append('search', ordersListParams.search);
    }

    params = params.append('pageIndex', ordersListParams.pageNumber);
    params = params.append('pageSize', ordersListParams.pageSize);
    params = params.append('sort', ordersListParams.sort);
    params = params.append('reversed', ordersListParams.orderReversed);

    return this.http.get<IOrdersListPagination>(this.baseUrl + 'orders', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getOrder(id: number) {
    return this.http.get<IOrder>(this.baseUrl + 'orders/' + id);
  }
}
