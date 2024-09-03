"use client";

import Image from "next/image";
import Link from "next/link";
import { useRouter } from "next/navigation";
import {useCart} from "../../context/cart/CartContext";
import {addUserOrder} from "../../api/order/addUserOrder";
import React, {useState} from "react";
import {useSession} from "next-auth/react";

const CartPage = () => {
    const { cart, removeItem, updateItemCount, clearCart } = useCart();
    const { data: session } = useSession();

    const router = useRouter();

    const [showPopup, setShowPopup] = useState(false);

    const totalAmount = cart.reduce((acc, item) => acc + item.price * item.count, 0);

    const handleOrder = async () => {
        try {
           const result = await addUserOrder({
                orderDate: new Date().toISOString(),
                orderItems: cart.map(i => {
                   return {
                        name: i.name,
                        price: i.price,
                        pictureUrl: i.pictureUrl,
                        gender: i.gender,
                        size: i.size,
                        catalogItemId: i.id,
                        count: i.count,
                    }
                }),
            }, session?.accessToken || "")

            if (result) {
                setShowPopup(true);

                setTimeout(() => {
                    setShowPopup(false);
                }, 3000);
            }

        } catch (error) {
            console.error("Error creating order:", error);
        }

        clearCart();
    };

    return (
        <>
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-2xl font-bold mb-6">Your Shopping Cart</h1>
            {cart.length === 0 ? (
                <p className="text-center">Your cart is empty. <Link href="/">Explore products</Link></p>
            ) : (
                <div className="grid grid-cols-1 gap-6">
                    {cart.map(item => (
                        <div key={item.id} className="flex items-center border p-4 rounded-md shadow-sm">
                            <Image src={item.pictureUrl} alt={item.name} width={80} height={80} className="rounded-md" />
                            <div className="ml-4 flex-1">
                                <h2 className="text-lg font-semibold">{item.name}</h2>
                                <p className="text-sm text-gray-500">Size: {item.size}</p>
                                <p className="text-sm text-gray-500">Price: ${item.price}</p>
                                <div className="flex items-center mt-2">
                                    <button onClick={() => updateItemCount(item.id, item.count - 1)} className="px-2 py-1 bg-gray-200 rounded">-</button>
                                    <span className="px-4">{item.count}</span>
                                    <button onClick={() => updateItemCount(item.id, item.count + 1)} className="px-2 py-1 bg-gray-200 rounded">+</button>
                                </div>
                            </div>
                            <button onClick={() => removeItem(item.id)} className="text-red-500">Remove</button>
                        </div>
                    ))}
                    <div className="flex justify-between items-center border-t pt-4 mt-4">
                        <h2 className="text-xl font-bold">Total: ${totalAmount}</h2>
                        <button onClick={handleOrder} className="bg-blue-500 text-white px-4 py-2 rounded">Place Order</button>
                    </div>
                </div>
            )}
        </div>
    {showPopup && (
        <div className="fixed bottom-4 right-4 bg-green-500 text-white p-4 rounded shadow-lg transition-transform transform">
            <p>Product added to cart!</p>
        </div>
    )}
    </>
    );
};

export default CartPage;
