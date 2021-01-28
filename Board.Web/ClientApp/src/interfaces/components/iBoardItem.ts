export default interface IBoardItem {
	id: string;
	orderNumber: number;
	isDone: boolean;
	description?: string;
	content?: string;
	isAdd?: boolean;
}
