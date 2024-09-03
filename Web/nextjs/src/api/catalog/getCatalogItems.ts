import {PaginatedItemsResponse} from "../../config/types/responses/paginatedItemsResponse";
import {Filter} from "../../config/types/requests/filter";

export const getCatalogItems = async (pageIndex: number, filters: Filter): Promise<PaginatedItemsResponse | null> => {
    const requestBody = {
        pageIndex: pageIndex,
        pageSize: 8,
        filters,
    };

    try {

        const response = await fetch("http://www.alevelwebsite.com:5001/api/v1/CatalogBff/Items", {
        cache: 'force-cache',
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody),
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