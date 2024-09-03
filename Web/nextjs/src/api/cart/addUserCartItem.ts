import {AddCartItemsRequest} from "../../config/types/requests/addCartItemsRequest";

export const updateUserCartItem = async (cartItems: AddCartItemsRequest, token: string): Promise<boolean | null> => {
    try {
        const response = await fetch("http://www.alevelwebsite.com:5004/api/v1/Basket/AddItem", {
        cache: 'force-cache',
        method: "POST",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        },
        body: JSON.stringify(cartItems)
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