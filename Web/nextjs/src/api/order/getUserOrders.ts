"use server"

import {Order} from "../../config/types/order/order";

export const getUserOrders = async (token: string): Promise<Order[] | null> => {
    try {
        const response = await fetch("http://www.alevelwebsite.com:5005/api/v1/OrderBff/GetAllUserOrders", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json",
            },
        });

        if (response.status === 204) {
            return null;
        }

        if (!response.ok) {
            console.error("Error during request:", response.statusText);
            return null;
        }

        return await response.json();
    } catch (error) {
        console.log(error);
        throw new Error("Failed to load items")
    }
};