import {AddOrderRequest} from "../../config/types/requests/addOrderRequest";

export const addUserOrder = async (order: AddOrderRequest, token: string): Promise<boolean | null> => {
    try {
        const response = await fetch("http://www.alevelwebsite.com:5005/api/v1/Order/AddOrder", {
            cache: 'force-cache',
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(order)
        });

        if (!response.ok) {
            console.error("Error during request:", response.statusText);
            return null;
        }

        return response.ok;
    } catch (error) {
        console.log(error);
        throw new Error("Failed to load items")
    }
};