"use client"
import Image from "next/image";
import {Order} from "../../config/types/order/order";
import {useSession} from "next-auth/react";
import {useEffect, useState} from "react";
import {getUserOrders} from "../../api/order/getUserOrders";

export const Orders = () => {
    const { data: session } = useSession();
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        const fetchOrders = async () => {
          const orders =  await getUserOrders(session?.accessToken || "")
            setOrders(orders || [])
        }

        fetchOrders()
    }, []);

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h2 className="text-2xl font-semibold mb-4">Order History</h2>
            {orders.length > 0 ? (
                orders.map((order) => (
                    <div key={order.id} className="mb-6 border border-gray-200 rounded-lg shadow-md">
                        <div className="p-4 bg-gray-100 border-b border-gray-200">
                            <p className="text-lg font-medium">
                                Order #{order.id} - {new Date(order.orderDate).toLocaleDateString()}
                            </p>
                            <p className="text-sm text-gray-600">Customer ID: {order.customerId}</p>
                            <p className="text-lg font-semibold mt-2">Total Amount: ${order.totalAmount}</p>
                        </div>
                        <div className="p-4">
                            {order.orderItems.map((item) => (
                                <div key={item.id} className="flex items-center py-2 border-b last:border-b-0">
                                    <div className="w-16 h-16 relative overflow-hidden rounded-md">
                                        <Image src={item.pictureUrl} alt={item.name} layout="fill" objectFit="cover" />
                                    </div>
                                    <div className="ml-4 flex-1">
                                        <h4 className="text-sm font-medium">{item.name}</h4>
                                        <p className="text-xs text-gray-500">{`Size: ${item.size}`}</p>
                                        <p className="text-sm">{`$${item.price} x ${item.count}`}</p>
                                    </div>
                                    <div className="text-right">
                                        <p className="text-sm font-semibold">{`$${item.price * item.count}`}</p>
                                    </div>
                                </div>
                            ))}
                        </div>
                    </div>
                ))
            ) : (
                <div className="text-center text-gray-500">No orders found.</div>
            )}
        </div>
    );
};

export default Orders;
