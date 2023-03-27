import {BASE_URL} from "../../common/constans/general.constants";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {AuthenticationService} from "./authentication.service";
import {Router} from "@angular/router";
import {RoutesEnum} from "../../common/enums/routes.enum";

export class ApiService {

  constructor(protected http: HttpClient, protected authService: AuthenticationService, protected router: Router) {
  }

  get headers() {
    if(this.authService.authenticated) {
      return {
        headers: new HttpHeaders({
          'Authorization': `Bearer ${this.authService.token}`
        })
      }
    }

    return undefined;
  }

  protected get<T>(methodUrl: string, params: any = undefined): Observable<T> {

    let url = `${BASE_URL}/${methodUrl}`;

    if(params){
      url = `${url}${this.resolveParams(params)}`;
    }

    return this.http.get<T>(url, this.headers).pipe(catchError(error => {
      return this.handleNotAuthorized(error);
    }));
  }

  protected post<T>(methodUrl: string, body: any): Observable<T> {
    return this.http.post<T>(`${BASE_URL}/${methodUrl}`, this.prepareRequestBody(body), this.headers).pipe(catchError(error => {
      return this.handleNotAuthorized(error);
    }));
  }

  protected put<T>(methodUrl: string, body: any): Observable<T> {
    return this.http.put<T>(`${BASE_URL}/${methodUrl}`, this.prepareRequestBody(body), this.headers).pipe(catchError(error => {
      return this.handleNotAuthorized(error);
    }));
  }



  protected delete<T>(methodUrl: string, params: any = undefined): Observable<T> {

    let url = `${BASE_URL}/${methodUrl}`;

    if(params) {
      url = `${url}${this.resolveParams(params)}`;
    }

    return this.http.delete<T>(url, this.headers).pipe(catchError(error => {
      return this.handleNotAuthorized(error);
    }));
  }

  private resolveParams(params: any): string {
    let result = '?';
    const paramsKeys = Object.keys(params);
    paramsKeys.forEach((key, index) => {
      if(paramsKeys.length - 1 == index) {
        result = `${result}${key}=${params[key]}`;
      }else {
        result = `${result}${key}=${params[key]}&`;
      }
    }) ;

    return result;
  }

  private prepareRequestBody(body: any): any {
    if (typeof body === 'object') {
      return new Proxy(body, {
        get(target, prop, receiver) {
          if (target[prop] instanceof Date) {
            return  new Date(
              Date.UTC(
                target[prop].getFullYear(),
                target[prop].getMonth(),
                target[prop].getDate(),
                target[prop].getHours(),
                target[prop].getMinutes()
              , target[prop].getSeconds()));
          }
          return target[prop]
        }
      })
    }

    return body
  }

  private handleNotAuthorized(error: any) {
    if (!!error.status && error.status === 401) {
      this.router.navigate([RoutesEnum.NoPermission]);
    }

    return throwError(error);
  }
}
