import { IAddress } from "./address"

export interface IOrderToCreate {
  basketId: string
  deliveryMethodId: number
  shipToAddress: IAddress
}

export interface IOrder {
  id: number
  buyerEmail: string
  orderDate: string
  orderDateFormatted: string
  shipToAddress: IAddress
  deliveryMethod: string
  deliveryTime: string
  shippingPrice: number
  orderItems: IOrderItem[]
  subtotal: number
  total: number
  status: string
}

export interface IOrderItem {
  productId: number
  productName: string
  pictureUrl: string
  price: number
  quantity: number
}
