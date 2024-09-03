import {CatalogItem} from "../../config/types/catalog/catalogItem";

export const getCatalogItemById = async (id: number): Promise<CatalogItem | null> => {
    try {

        const response = await fetch(`http://www.alevelwebsite.com:5001/api/v1/CatalogBff/ItemById?id=${id}`, {
            cache: 'force-cache',
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            }
        });

        if (!response.ok) {
            console.error("Error during request:", response.statusText);
            return null;
        }

        return await response.json();
    } catch (error) {
        throw new Error("Failed to load items")
    }
};