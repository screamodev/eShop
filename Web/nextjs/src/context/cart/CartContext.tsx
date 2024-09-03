"use client";

import {createContext, useContext, useState, ReactNode, useEffect} from "react";
import {CartItem} from "../../config/types/cart/cartItem";
import {getUserCartItems} from "../../api/cart/getUserCartItems";
import {useSession} from "next-auth/react";
import {updateUserCartItem} from "../../api/cart/addUserCartItem";

type CartContextType = {
    cart: CartItem[];
    addItem: (item: CartItem) => void;
    removeItem: (id: number) => void;
    updateItemCount: (id: number, count: number) => void;
    clearCart: () => void;
    totalAmount: number;
};

const CartContext = createContext<CartContextType | undefined>(undefined);

export const CartProvider = ({ children }: { children: ReactNode }) => {
    const [cart, setCart] = useState<CartItem[]>([]);
    const [isFirstLoad, setIsFirstLoad] = useState(false);
    const { data: session } = useSession();

    useEffect(() => {
        const fetchCart = async () => {
            try {
                const response = await getUserCartItems(session?.accessToken || "");
                setCart(response?.catalogItems || []);
                setIsFirstLoad(true);
            } catch (error) {
                console.error("Failed to fetch cart:", error);
            }
        };

        fetchCart();
    }, []);

    useEffect(() => {
        const updateCart = async () => {
            if (isFirstLoad) {
                try {
                    await updateUserCartItem({ catalogItems: cart }, session?.accessToken || "");
                } catch (error) {
                    console.error("Failed to update cart:", error);
                }
            }
        };

        updateCart();
    }, [cart, isFirstLoad]);

    const addItem = (item: CartItem) => {
        setCart((prevCart) => {
            const existingItem = prevCart.find(cartItem => cartItem.id === item.id);
            if (existingItem) {
                return prevCart.map(cartItem =>
                    cartItem.id === item.id ? { ...cartItem, count: cartItem.count + item.count } : cartItem
                );
            }
            return [...prevCart, item];
        });
    };

    const removeItem = (id: number) => {
        setCart((prevCart) => prevCart.filter((item) => item.id !== id));
    };

    const updateItemCount = (id: number, count: number) => {
        setCart((prevCart) =>
            prevCart.map((item) =>
                item.id === id ? { ...item, count } : item
            )
        );
    };

    const clearCart = () => {
        setCart([]);
    };

    const totalAmount = cart.reduce((acc, item) => acc + item.price * item.count, 0);

    return (
        <CartContext.Provider
            value={{ cart, addItem, removeItem, updateItemCount, clearCart, totalAmount }}
        >
            {children}
        </CartContext.Provider>
    );
};

export const useCart = () => {
    const context = useContext(CartContext);
    if (!context) {
        throw new Error("useCart must be used within a CartProvider");
    }
    return context;
};
