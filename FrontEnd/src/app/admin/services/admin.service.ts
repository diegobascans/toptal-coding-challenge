import {Injectable} from "@angular/core";
import {ApiService} from "../../shared/services/api.service";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "../../shared/services/authentication.service";
import {GeneralInformationResponse} from "./method-parameters/general-information-response";
import {User} from "../../common/classes/user";
import {Meal} from "../../common/classes/meal";
import {Router} from "@angular/router";
import {DATE_FORMAT_SERVICE, LOCALE} from "../../common/constans/general.constants";
import {DatePipe} from "@angular/common";

interface GetGeneralInformationInput {
  pageNumber: number,
  start: string | null,
  end: string | null,
}

@Injectable()
export class AdminService extends ApiService {

  datePipe: DatePipe;

  constructor(http: HttpClient, authService: AuthenticationService, router: Router) {
    super(http, authService, router);
    this.datePipe = new DatePipe(LOCALE);
  }

  public getGeneralInformation(pageNumber: number, startDate: Date, endDate: Date): Observable<GeneralInformationResponse> {
    const input:GetGeneralInformationInput = {
      pageNumber: pageNumber,
      start: this.datePipe.transform(startDate, DATE_FORMAT_SERVICE),
      end: this.datePipe.transform(endDate, DATE_FORMAT_SERVICE),
    }

    return this.get<GeneralInformationResponse>('admin/getGeneralInformation', input);
  }

  public getUsers(): Observable<User[]> {
    return this.get<User[]>('admin/getUsers');
  }

  public deleteMeal(mealId: number): Observable<void> {
    return this.delete<void>('admin/deleteMeal', {mealId: mealId});
  }

  public updateMeal(meal: Meal): Observable<void> {
    return this.put<void>('admin/updateMeal', meal);
  }

  public createMeal(meal: Meal): Observable<void> {
    return this.post<void>('admin/createMeal', meal);
  }
}
