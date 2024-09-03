"use server"
import {GetUserCartItemsResponse} from "../../config/types/responses/getUserCartItemsResponse";

export const getUserCartItems = async (token: string): Promise<GetUserCartItemsResponse | null> => {
    try {
        const response = await fetch("http://www.alevelwebsite.com:5004/api/v1/BasketBff/GetItems", {
            method: "POST",
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