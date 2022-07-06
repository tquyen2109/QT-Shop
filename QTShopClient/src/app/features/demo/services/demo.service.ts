import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {DemoModel} from "../models/DemoModel";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class DemoService {
  catalogUrl = environment.catalogApiURI;
  constructor(private http: HttpClient) { }
  getDemoData() : Observable<DemoModel[]> {
    return this.http.get<DemoModel[]>(this.catalogUrl + 'product');
  };
}
