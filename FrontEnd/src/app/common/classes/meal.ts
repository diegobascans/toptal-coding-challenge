import {Food} from "./food";
import {User} from "./user";

export class Meal {
  id?: number;
  date: Date;
  calories: number;
  foodId: number;
  food?: Food;
  userId?: number;
  user?: User
}
