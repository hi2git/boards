export default interface IPost {
	id: string;
	orderNumber: number;
	isDone: boolean;
	description?: string;
	content?: string;
	isAdd?: boolean;
}
