import {CatalogItem} from "../catalog/catalogItem";

export type PaginatedItemsResponse = {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: CatalogItem[];
};