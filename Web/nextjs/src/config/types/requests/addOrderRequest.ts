import {AddOrderItemRequest} from "./addOrderItemRequest";

export type AddOrderRequest = {
    orderDate: string;
    orderItems: AddOrderItemRequest[];
};