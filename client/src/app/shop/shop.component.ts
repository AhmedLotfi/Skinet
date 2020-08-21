import { Component, OnInit } from "@angular/core";
import { IProduct } from "../shared/models/products";
import { ShopService } from "./shop.service";
import { IType } from "../shared/models/productTypes";
import { IBrand } from "../shared/models/brands";

@Component({
  selector: "app-shop",
  templateUrl: "./shop.component.html",
  styleUrls: ["./shop.component.scss"],
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  types: IType[];
  brands: IBrand[];
  brandIdSelected: number = 0;
  typeIdSelected: number = 0;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService
      .getProducts(this.brandIdSelected, this.typeIdSelected)
      .subscribe(
        (response) => {
          this.products = response.data;
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
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeIdSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }
}
