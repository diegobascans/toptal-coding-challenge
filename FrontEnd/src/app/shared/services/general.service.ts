import {Injectable} from "@angular/core";
import {ApiService} from "./api.service";
import {Food} from "../../common/classes/food";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "./authentication.service";
import {Authentication} from "../../common/classes/authentication";
import {Router} from "@angular/router";

@Injectable({providedIn: 'root',})
export class GeneralService extends ApiService {

  constructor(http: HttpClient, authService: AuthenticationService, router: Router) {
    super(http, authService, router);
  }

  public getFoods(): Observable<Food[]> {
    return this.get<Food[]>('general/getFoods')
  }

  public authenticate(userId: number): Observable<Authentication> {
    return this.get<Authentication>(`general/authenticate/${userId}`);
  }

}
