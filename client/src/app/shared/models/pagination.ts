import { IOrder } from "./order"
import { IProduct } from "./product"

export interface IPagination {
  pageIndex: number
  pageSize: number
  count: number
  data: IProduct[]
}

export interface IOrdersListPagination {
  pageIndex: number
  pageSize: number
  count: number
  data: IOrder[]
}
