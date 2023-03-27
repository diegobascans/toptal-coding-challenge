import {User} from "../../common/classes/user";
import {Food} from "../../common/classes/food";

export interface AdminElement {
  id: number,
  date: Date,
  calories: number,
  user?: User,
  food?: Food,
}
