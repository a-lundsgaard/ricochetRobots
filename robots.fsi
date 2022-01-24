module Robots
type Direction =
  | North
  | South
  | East
  | West
type Position = int * int
type Action =
  | Stop of Position
  | Continue of Direction * Position
  | Ignore
type BoardDisplay =
  class
    new : rows:int * cols:int -> BoardDisplay
    member Set : row:int * col:int * cont:string -> unit
    member SetBottomWall : row:int -> col:int -> unit
    member SetRightWall : row:int -> col:int -> unit
    member Show : unit -> unit
  end
[<AbstractClassAttribute ()>]
type BoardElement =
  class
    new : unit -> BoardElement
    abstract member GameOver : Robot list -> bool
    abstract member Interact : Robot -> Direction -> Action
    abstract member RenderOn : BoardDisplay -> unit
    override GameOver : Robot list -> bool
    override Interact : Robot -> Direction -> Action
  end
and Robot =
  class
    inherit BoardElement
    new : row:int * col:int * name:string -> Robot
    override Interact : other:Robot -> dir:Direction -> Action
    override RenderOn : display:BoardDisplay -> unit
    member Step : dir:Direction -> Position
    member Name : string
    member Position : Position
    member Position : Position with set
  end
type Goal =
  class
    inherit BoardElement
    new : r:int * c:int -> Goal
    override GameOver : robotList:Robot list -> bool
    override RenderOn : display:BoardDisplay -> unit
  end
type VerticalWall =
  class
    inherit BoardElement
    new : r:int * c:int * n:int -> VerticalWall
    override Interact : other:Robot -> dir:Direction -> Action
    override RenderOn : display:BoardDisplay -> unit
  end
type HorizontalWall =
  class
    inherit BoardElement
    new : r:int * c:int * n:int -> HorizontalWall
    override Interact : other:Robot -> dir:Direction -> Action
    override RenderOn : display:BoardDisplay -> unit
  end
type BoardFrame =
  class
    inherit BoardElement
    new : r:int * c:int -> BoardFrame
    override Interact : other:Robot -> dir:Direction -> Action
    override RenderOn : display:BoardDisplay -> unit
  end
type Board =
  class
    new : r:int * c:int -> Board
    member AddElement : element:BoardElement -> unit
    member AddRobot : robot:Robot -> unit
    member GetRobot : unit -> Robot option
    member IsGameOver : unit -> bool
    member Move : robot:Robot * dir:Direction -> unit
    member RenderView : unit -> unit
    member Elements : BoardElement list
    member Robots : Robot list
  end
type Teleport =
  class
    inherit BoardElement
    new : r:int * c:int * board:Board -> Teleport
    override Interact : robot:Robot -> dir:Direction -> Action
    override RenderOn : display:BoardDisplay -> unit
  end
type Game =
  class
    new : board:Board -> Game
    member GetPlayerName : player:string -> string
    member Play : unit -> int
    member ReadHighScore : unit -> string option
    member WriteHighScore : moves:int -> player:string -> unit
  end

