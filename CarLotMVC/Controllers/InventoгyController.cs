using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLotDAL.EF;
using AutoLotDALModels.Models;
using AutoLotDAL.Repos;
using System.Data.Entity.Infrastructure;
namespace CarLotMVC.Controllers
{
    /*
     * Как обсуждалось ранее, когда запрос поступает от браузера, он (обычно) отображается на метод действия из 
     * определенного класса контроллера. Контроллер — это класс, который унаследован от 
     * одного из двух абстрактных классов, Controller и AsyncController.
     * Обратите внимание, что вы также можете создать контроллер с нуля, реализовав интерфейс IController.
     * Метод действия — это открытый метод в классе контроллера. */

    /*Действия обычно возвращают объект типа ActionResult (или типа Task <ActionResult> для асинхронных операций).
     * Существует несколько производных от ActionResult типов, и некоторые часто используемые 
     * из них кратко описаны в табл. 29.8.
     * 
     *  Таблица 29.8. Обычные классы, производные от ActionResult
        Класс                                Описание
        ViewResult                           Возвращают в качестве веб-страницы представление
        PartialViewResult                    (или частичное представление)

        RedirectResult                       Перенаправляют на другое действие
        RedirectToRouteResult

        JsonResult                           Возвращает клиенту сериализированный результат JSON

        FileResult                           Возвращает клиенту содержимое двоичного файла

        ContentResult                        Возвращает клиенту тип содержимого, определенный
                                             пользователем

        HttpStatusCodeResult                 Возвращает специфический код состояния HTTP 
    */




    /* Описание того как добавить контроллер средствами Visual Studio
     * 
     * Чтобы понять контроллеры и действия, лучше всего добавить новый контроллер с действиями с применением средств, 
     * встроенных в Visual Studio. Щелкнем правой кнопкой мыши на папке Controllers в проекте и выберем в контекстном 
     * меню пункт Add^Controller (Добавить^Контроллер),
     * 
     * В результате откроется диалоговое окно Add Scaffold (Добавление шаблона), 
     * 
     * Здесь доступно несколько вариантов, среди которых(Для этого проекта) нужно выбрать MVC 5 Controller with views, 
     * using Entity Framework (Контроллер MVC 5 с представлениями, использующий Entity Framework). 
     * 
     * Откроется диалоговое окно Add Controller (Добавление контроллера), которое позволяет указывать типы для
     * контроллера и методов действий 
     * 
     * Первым делом необходимо указать класс модели, где будет
     * определен тип контроллера и методы действий. В раскрывающемся списке Model class (Класс модели) выберем Inventory.
     * 
     * Далее понадобится указать класс контекста. Если он не выбран, то будет создан новый такой класс. 
     * В списке Data context class (Класс контекста данных) выберем AutoLotEntities. 
     * 
     * Затем нужно указать, должны ли применяться асинхронные методы действий, что зависит от потребностей 
     * разрабатываемого проекта. В текущем примере флажок Use async controller 
     * actions (Использовать асинхронные действия контроллера) мы отмечать не будем.
     * 
     * Флажок Generate views (Генерировать представления), отмеченный по умолчанию, указывает на необходимость создания 
     * связанного представления для каждого метода действия. 
     * 
     * Флажок Reference script libraries (Ссылаться на библиотеки сценариев) заставляет включить в представления 
     * соответствующие сценарии. 
     * 
     * Флажок Use a layout page (Использовать страницу компоновки) обсуждается позже в главе. 
     * 
     * Оставим флажки Generate views, Reference script libraries и Use a layout page отмеченными
     * и в поле Controller name (Имя контроллера) изменим имя на InventoгyController (вместо InventorlesController).
     * 
     * В результате было решено несколько задач. В папке Controllers создан класс InventoryController. Кроме того, 
     * в папке Views создана папка Inventory, а в нее добавлено пять представлений.
     */

    /*На заметку! В среде Visual Studio доступно немало инструментов, оказывающих содействие разработке приложений MVC. 
     * Вы только что видели, как создавать новый контроллер с помощью диалогового окна Add Controller. Если щелкнуть правой 
     * кнопкой мыши на папке Views, то в контекстном меню будет присутствовать пункт для добавления нового представления. 
     * Щелкнув правой кнопкой мыши на действии, можно добавить новое представление (которое будет помещено в папку
     * Views/Controller и получит такое же имя, как у действия) или перейти к подходящему представлению. Все возможности
     * подобного рода опираются на соглашения, которые обсуждались ранее, так что если
     * вы следуете правилам, то все будет в порядк*/






    /*по соглашению имя класса контроллера заканчивается словом Controller. Кроме того, класс является производным от 
     * абстрактного класса Controller. В нем имеется набор методов (действий), таких как Index (), Edit () и т.д. Мы 
     * исследуем их по очереди вместе с атрибутами, которыми они декорированы. Наконец, метод Dispose () переопределен,
     * чтобы можно было освобождать любые дорогостоящие ресурсы (вроде экземпляра класса Context из EF),
     * задействованные контроллером.*/
    public class InventoгyController : Controller
    {
        /*В первой строке определения класса InventoryController создается новый экземпляр AutoLotEntities, как было указано 
         * при создании контроллера. Его понадобится заменить классом InventoryRepo.*/
        private InventoryRepo db = new InventoryRepo();





        /*Действие Index получает все записи Inventory и возвращает данные представлению. 
         * Модифицируем вызов для работы с классом InventoryRepo*/
        // GET: Inventoгy
        public ActionResult Index()
        {
            /*
             * В коде вызывается перегруженный метод View () базового класса Controller, который возвращает новый объект
             * ViewResult. Когда имя представления не указано (как в данном случае), то по соглашению представление
             * именуется согласно методу действия и помещается в папку с именем контроллера, т.е. 
             * Views/Inventory/Index.cshtml. Имя представления можно изменить, передавая методу View() желаемое имя.
             * 
             */
            return View(db.GetAll());
        }



        /*Метод действия Details () возвращает все подробности для одной записи Inventory. При этом URL в 
         * формате http://mysite.eom/Inventory/Details/5 будет отображен на метод действия Details() класса 
         * InventoryController с параметром по имени id, которому передается значение 5.*/
        // GET: Inventoгy/Details/5
        public ActionResult Details(int? id)
        {
            /*В таком просто выглядящем методе есть пара интересных моментов. Вспомните из обсуждения маршрута, что параметр 
             * id является необязательным, поэтому URL вида /Inventory/Details будет корректно отображаться на данный метод.
             * Запустив приложение и введя Inventory/Details (без части id в URL), вы получите экран ошибки. 
             * Подобным же образом, если запись Inventory найти не удалось, то метод действия
             * возвращает код состояния HTTP 404.*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }


        /*Распространенная проблема веб-сайтов связана с тем, что пользователи щелкают на кнопке отправки внутри формы два 
         * раза, потенциально дублируя любое изменение, которое поддерживается страницей. Инфраструктурой MVC взят на вооружение
         * паттерн, который значительно смягчает указанную проблему, но перед его обсуждением важно понять два метода HTTP
         *  используемые в веб-приложениях MVC.*/


        /*Протокол HTTP определяет вызов HttpGet как запрос данных из сервера, а вызов HttpPost — как отправку данных 
         * специфическому ресурсу на обработку. В инфраструктуре MVC любое действие без атрибута HTTP (вроде HttpPost) 
         * будет выполняться как операция HttpGet.*/

        /*Чтобы предотвратить дублирование отправок формы, в MVC применяется паттерн “Отправка-перенаправление-получение” 
         * (Post-Redirect-Get — PRG), который появился раньше MVC и широко использовался разработчиками веб-приложений. 
         * Он предусматривает перенаправление каждого успешного действия HttpPost на действие HttpGet, так что если 
         * пользователь щелкнет на кнопке отправки еще раз, то лишь обновится страница, выданная действием 
         * HttpGet, а повторная отправка не произойдет.*/






        /*Действие Create реализует паттерн PRG с применением двух методов Create (); один не принимает параметров,
         * а другой принимает в качестве параметра объект Inventory.*/

        // GET: Inventoгy/Create
        public ActionResult Create()
        {
            /*Метод Create () без параметров обрабатывает запрос HttpGet и просто представляет страницу, которая
             * позволяет пользователю указать информацию, необходимую для создания записи.*/
            return View();
        }



        /*Данная версия выполняется, когда пользователь щелкнул на кнопке отправки формы Create (при условии, 
         * что все проверки достоверности на стороне клиента прошли успешно).*/

        // POST: Inventoгy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        /*Одним из средств защиты от взлома является атрибут AntiForgeryToken, который добавляет специальное значение формы. 
         * При поступлении запроса HttpPost инфраструктура MVC проверяет такое значение формы на стороне сервера (при условии,
         * что в методе действия присутствует атрибут [ValidateAntiForgeryToken]). Хотя это не единственная мера защиты,
         * каждая форма должна добавлять значение AntiForgeryToken и каждое действие HttpPost должно проверять его.*/
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*Атрибут [Bind] в методах HttpPost позволяет помещать свойства в “белый” или “черный” 
                                    * список, чтобы предотвращать атаки чрезмерной отправкой данных (over-posting) 
                                    * пользователем. Когда поля находятся в “белолГ списке, только им будут присвоены 
                                    * значения во время привязки модели. Помещение в "черный" список исключает свойства из
                                    * процесса привязки модели. */[Bind(Include = "Make,Color,PetName")] Inventory inventory)
        {
            /*Привязка модели берет все пары “имя-значение” из HTML-формы (включая форму, строку запроса и т.д.) и пытается 
             * воссоздать объект указанного типа, используя рефлексию, за счет сопоставления имен свойств с именами в парах
             * “имя-значение” и затем присваивает значения. Если одно или большее число значений присвоить не удается (скажем,
             * из-за проблем с преобразованием типа данных или ошибок проверки достоверности), тогда механизм привязки
             * моделей укажет на ошибку, устанавливая свойство ModelState.IsValid в false.*/
            if (!ModelState.IsValid)return View(inventory);

            try
            {
                db.Add(inventory);
            }catch(Exception ex)
            {
                /*В дополнение к свойству IsValid свойство ModelState имеет тип ModelStateDictionary и содержит информацию 
                 * об ошибках для каждого проблемного свойства, а также информацию об ошибках уровня модели. 
                 * 
                 * Чтобы добавить сообщение об ошибке для целой модели, вместо имени свойства
                 * должно использоваться значение string.Empty
                 * 
                 * Существует явная и неявная привязка моделей. Для явной привязки модели необходимо вызвать метод TryUpdateModel(),
                 * передав ему экземпляр типа. Если привязка модели не удалась, то вызов TryUpdateModel () возвращает
                 * false; подобно неявной привязке моделей он также добавляет сведения о любых ошибках
                 * привязки в коллекцию ModelState.
                 * 
                 * В случае неявной привязки модели желаемый тип применяется в качестве параметра для метода.
                 * 
                 * В представлениях Create, Delete и Edit, если модель находится в недопустимом состоянии (ModelState.IsValid 
                 * равно false), то пользователь возвращается к тому же самому представлению для повторения попытки с 
                 * восстановлением всех ранее введенных данных. Когда состояние модели допустимо (ModelState.IsValid 
                 * равно true), метод должен сохранить запись в базе данных и отправить пользователя на
                 * действие Index (метод HttpGet)
                 * 
                 
                 
                 */
                ModelState.AddModelError(string.Empty,
                                                    $@"Unable to create record: {ex.Message}");
                                                     // He удается создать запись.
                return View(inventory);
            }
            /*Если все прошло успешно, то метод действия возвращает результат вызова RedirectToAction(), обеспечивая
             * перенаправление пользователя на метод действия Index() класса InventoryController*/
            return RedirectToAction("Index");

        }








        /*Подобно методу действия Create () процесс редактирования применяет паттерн PRG, реализованный с 
         * использованием двух версий метода действия Edit ()*/

        // GET: Inventoгy/Edit/5
        public ActionResult Edit(int? id)
        {
            /*Первая версия метода Edit () принимает параметр id, получает запись Inventory 
         * и возвращает подходящее представление. За исключением возвращения другого представления
         * она идентична методу Details ().*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }


        /*Вторая версия метода Edit () выполняется, когда пользователь щелкнул на кнопке отправки формы редактирования 
         * (при условии успешного прохождения всех проверок достоверности на стороне клиента). Если состояние модели не 
         * является допустимым, то метод еще раз возвращает представление Edit, отправляя текущие значения для объекта
         * Inventory. Если состояние модели допустимо, тогда объект Inventory передается хранилищу для попытки его записи. 
         */

        // POST: Inventoгy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        {
            if (ModelState.IsValid)return View(inventory);
            try
            {
                db.Save(inventory);
            }catch(DbUpdateConcurrencyException ex)
            {/*исключения DbUpdateConcurrencyException, будет сгенерировано, если другой
              * пользователь обновил запись после того, как текущий пользователь загрузил ее на веб-страницу.*/
                ModelState.AddModelError(string.Empty,
                                            $@"Unable to save the record. Another user has updated it.
                                                            {ex.Message}");
                                                // He удается сохранить запись. Другой пользователь обновил ее.
                return View(inventory);
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record.
                                {ex.Message}"); // He удается сохранить запись.
                return View(inventory);
            }
            /*Если все прошло успешно, то метод действия возвращает результат вызова RedirectToAction(), обеспечивая
             * перенаправление пользователя на метод действия Index() класса InventoryController*/
            return RedirectToAction("Index");
        }






        // GET: Inventoгy/Delete/5
        public ActionResult Delete(int? id)
        {
            /*Первая версия метода Delete () принимает параметр id и идентична версиям HttpGet методов Details() и Edit ().*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }




        /*Вторая версия Delete () выполняется, когда пользователь щелкнул на кнопке отправки формы удаления. Автоматически
         * сгенерированная версия этого метода принимает только id в качестве параметра, т.е. она имеет ту же самую сигнатуру,
         * что и версия HttpGet метода. Поскольку не разрешено иметь два метода с одним и тем же именем и сигнатурой,
         * генерируемый метод именуется как DeleteConfirmed () и снабжается атрибутом [ActionName ("Delete") ].
         * 
         * Изначально было так:
         *  [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed([Bind(Include = "Id")] Inventory inventory)
         * 
         * Модифицируем сигнатуру метода следующим образом (можно также удалить атрибут ActionName, раз уж метод
         * переименован на Delete ())
         * 
         * 
         * Библиотека AutoLotDAL проверяет наличие конфликтов параллелизма и для удаления записи помимо свойства Id 
         * требует указания свойства Timestamp. Именно потому параметр int id изменяется на Inventory inventory. 
         * 
         */

        // POST: Inventoгy/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(/*Атрибут [Bind] со строкой "Id, Timestamp" в Include обеспечит помещение в 
                                    * экземпляр Inventory только указанных значений
                                    * и игнорирование остальных свойств.*/[Bind(Include = "Id,Timestamp")] Inventory inventory)
        {
            try
            {
                db.Delete(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record.
                                                        Another user updated the record. {ex.Message}");
                // He удается удалить запись. Другой пользователь обновил ее.
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record:
                                            {ex.Message}"); // He удается удалить запись.
            }
            return RedirectToAction("Index");
        }




        //Переопределенный метод Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
