using Demo.Data.Repository;
using Demo.Domain.UseCase;
using Demo.UI;

// Создаем экземпляр репозиториев
GroupRepositoryImpl groupRepositoryImpl = new GroupRepositoryImpl();
UserRepositoryImpl userRepositoryImpl = new UserRepositoryImpl();
PresenceRepositoryImpl presenceRepositoryImpl = new PresenceRepositoryImpl(); // Создаем экземпляр PresenceRepositoryImpl

// Создаем UseCase для пользователей и групп
UserUseCase userUseCase = new UserUseCase(userRepositoryImpl, groupRepositoryImpl);
GroupUseCase groupUseCase = new GroupUseCase(groupRepositoryImpl);
UseCaseGeneratePresence presenceUseCase = new UseCaseGeneratePresence(userRepositoryImpl, presenceRepositoryImpl);

// Создаем пользовательский интерфейс
MainMenuUI mainMenuUI = new MainMenuUI(userUseCase, groupUseCase, presenceUseCase);

// Выводим главное меню
mainMenuUI.DisplayMenu();
