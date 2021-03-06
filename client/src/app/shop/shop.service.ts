import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { IPagination } from "../shared/models/pagination";
import { IBrand } from "../shared/models/brands";
import { IType } from "../shared/models/productTypes";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ShopParams } from "../shared/models/shopParams";

@Injectable({
  providedIn: "root",
})
export class ShopService {
  baseUrl = "https://localhost:5001/api";

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append("brandId", shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append("typeId", shopParams.typeId.toString());
    }

    params = params.append("sort", shopParams.sort);
    params = params.append("pageIndex", shopParams.pageNumber.toString());
    params = params.append("pageSize", shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(`${this.baseUrl}/Products`, {
        observe: "response",
        params,
      })
      .pipe(
        map((response) => {
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
