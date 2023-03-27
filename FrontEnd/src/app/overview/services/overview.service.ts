import {Injectable} from "@angular/core";
import {Food} from "../../common/classes/food";
import {BehaviorSubject, Subject} from "rxjs";
import {GeneralService} from "../../shared/services/general.service";

@Injectable()
export class OverviewService {
  private _foods: Food[];
  public foodsUpdated: BehaviorSubject<Food[]>;

  get foods(): Food[] {
    return this._foods;
  }

  constructor(private generalService: GeneralService) {
    this.foodsUpdated = new BehaviorSubject<Food[]>(this._foods);

    this.generalService.getFoods().subscribe((foods: Food[]) => {
      this._foods = foods
      this.foodsUpdated.next(this._foods);
    })
  }
}
