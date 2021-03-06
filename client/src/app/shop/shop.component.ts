import { Component, OnInit } from "@angular/core";
import { IProduct } from "../shared/models/products";
import { ShopService } from "./shop.service";
import { IType } from "../shared/models/productTypes";
import { IBrand } from "../shared/models/brands";
import { ShopParams } from "../shared/models/shopParams";

@Component({
  selector: "app-shop",
  templateUrl: "./shop.component.html",
  styleUrls: ["./shop.component.scss"],
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  types: IType[];
  brands: IBrand[];
  shopParams = new ShopParams();
  totalCount: number;

  sortOptions = [
    {
      name: "Alphabetical",
      value: "name",
    },
    {
      name: "Price : Low to high",
      value: "priceAsc",
    },
    {
      name: "Price : High to low",
      value: "priceDesc",
    },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      (error) => console.log(error)
    );
  }

  getBrands() {
    this.shopService.getBrands().subscribe(
      (response) => {
        this.brands = [{ id: 0, name: "All" }, ...response];
      },
      (error) => console.log(error)
    );
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: "All" }, ...response];
      },
      (error) => console.log(error)
    );
  }

  onBrandIdSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeIdSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    this.shopParams.pageNumber = event.page;
    this.getProducts();
  }
}
