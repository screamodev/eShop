import {OrderItem} from "./orderItem";

export type Order = {
    id: number;
    orderDate: string;
    totalAmount: number;
    customerId: number;
    orderItems: OrderItem[];
};