import {CatalogGender} from "../../config/types/catalog/catalogGender";

export const getGenders = async (): Promise<CatalogGender[] | null> => {
    try {
        const response = await fetch("http://www.alevelwebsite.com:5001/api/v1/CatalogBff/Genders", {
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