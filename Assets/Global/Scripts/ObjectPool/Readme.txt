ObjectPool使用方法

Poolの作成
参考 => SampleのBulletを参照。

使用者
参考 => SampleのUserを参照。


Pool<Mono>について

記述必須
SetMono(Poolを作成するための元のObject, 生成個数); Poolの作成。
CreateRequest(); 全ての設定を記述後に呼び出す。

記述任意
IsSetParent(Poolの親Object); 親の有無を設定。有なら、親Objectの子にセット。