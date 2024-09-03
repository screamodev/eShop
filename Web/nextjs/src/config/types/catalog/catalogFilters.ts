import {CatalogBrand} from "./catalogBrand";
import {CatalogGender} from "./catalogGender";
import {CatalogType} from "./catalogType";
import {CatalogSize} from "./catalogSize";

export type CatalogFilters = {
    brands: CatalogBrand[] | null
    genders: CatalogGender[] | null
    types: CatalogType[] | null
    sizes: CatalogSize[] | null
}