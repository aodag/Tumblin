(function() {
    var app = {
        controller() {
            var vm = {
                posts: m.prop([])
            };
            m.request({
                method: "GET",
                url: "/api/posts"
            }).then(vm.posts);
            return vm;
        },
        view(ctrl) {
            var posts = ctrl.posts();
            return m("main", [
                m("section", posts.map((post) => {
                    return m("div", [
                        m("h2", m("a", {href: post.id}, post.title)),
                        m("p", post.text)
                    ]);
                }))
            ]);
        }
    };
    function init() {
        m.mount(document.body, m.component(app));
    }
    function main() {
        window.addEventListener("load", init);
    }
    main();
}());