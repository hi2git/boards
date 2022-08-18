import IdName from "./iIdName";
import Pageable from "./iPageable";
import PostFilter from "./iPostFilter";
import Post from "./iPost";
import UserLogin from "./iUserLogin";
import UserSettings from "./iUserSettings";

export type IIdName = IdName;
export type IPageable<T> = Pageable<T>;
export type IPostFilter = PostFilter;
export type IPost = Post;
export type IUserLogin = UserLogin;
export type IUserSettings = UserSettings;
