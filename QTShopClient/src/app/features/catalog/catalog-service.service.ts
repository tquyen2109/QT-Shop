import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Product} from "./models/Product";

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  catalogUrl = environment.catalogApiURI;
  constructor(private http: HttpClient) { }
  getProducts() : Observable<Product[]> {
    return this.http.get<Product[]>(this.catalogUrl + 'product');
  };
}
