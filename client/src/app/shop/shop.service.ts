import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { IPagination } from "../shared/models/pagination";
import { IBrand } from "../shared/models/brands";
import { IType } from "../shared/models/productTypes";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class ShopService {
  baseUrl = "https://localhost:5001/api";

  constructor(private http: HttpClient) {}

  getProducts(brandId?: number, typeId?: number) {
    let params = new HttpParams();

    if (brandId) {
      params = params.append("brandId", brandId.toString());
    }

    if (typeId) {
      params = params.append("typeId", typeId.toString());
    }

    console.log(brandId, typeId, params);

    return this.http
      .get<IPagination>(`${this.baseUrl}/Products`, {
        observe: "response",
        params,
      })
      .pipe(
        map((response) => {
          console.log(response.body.data.length);
          return response.body;
        })
      );
  }

  getBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(
      `${this.baseUrl}/Products/GetProductsBrands`
    );
  }

  getTypes() {
    return this.http.get<IType[]>(`${this.baseUrl}/Products/GetProductsTypes`);
  }
}
