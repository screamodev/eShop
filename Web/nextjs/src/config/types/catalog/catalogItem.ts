import {CatalogType} from "./catalogType";
import {CatalogBrand} from "./catalogBrand";
import {CatalogGender} from "./catalogGender";
import {CatalogSize} from "./catalogSize";

export type CatalogItem = {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    catalogType: CatalogType;
    catalogBrand: CatalogBrand;
    catalogGender: CatalogGender;
    catalogItemSizes: CatalogSize[];
    availableStock: number;
};