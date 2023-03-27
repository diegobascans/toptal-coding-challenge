import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ApiService} from "../../shared/services/api.service";
import {AuthenticationService} from "../../shared/services/authentication.service";
import {DatePipe} from "@angular/common";
import {Meal} from "../../common/classes/meal";
import {DATE_FORMAT_SERVICE, LOCALE} from "../../common/constans/general.constants";
import {Router} from "@angular/router";
import {GetUserMealsResponse} from "./method-parameters/get-user-meals-response";
import {CheatedFood} from "../classes/cheated-food";
import {Food} from "../../common/classes/food";

interface GetUserMealsInput {
  pageNumber: number,
  start: string | null,
  end: string | null
}


@Injectable()
export class UserService extends ApiService {

  datePipe: DatePipe;

  constructor(http: HttpClient, authService: AuthenticationService, router: Router) {
    super(http, authService, router);
    this.datePipe = new DatePipe(LOCALE);
  }

  public getUserMeals(pageNumber: number, startDate: Date, endDate: Date): Observable<GetUserMealsResponse> {
    const input: GetUserMealsInput = {
      pageNumber: pageNumber,
      start: this.datePipe.transform(startDate, DATE_FORMAT_SERVICE),
      end: this.datePipe.transform(endDate, DATE_FORMAT_SERVICE)
    }
    return this.get<GetUserMealsResponse>('user/getUserMeals', input);
  }

  public addMeal(meal: Meal): Observable<void> {
    return this.post<void>('user/addMeal', meal);
  }

  public getCheatedFoods(): Observable<CheatedFood[]> {
    return this.get<CheatedFood[]>('user/getCheatedFoods');
  }

  public updateCheatedFoods(foods: Food[]): Observable<void> {
    return this.put<void>('user/updateCheatedFoods', foods);
  }
}
