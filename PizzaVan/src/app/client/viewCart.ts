export interface Cart {
  CusId: number;
  ListViewCart: Item[];
  TotalPrice: number;
}

export interface Item {
  Dish: object;
  Quantity: number;
  Total: number;
}

export class ViewCart{
}
